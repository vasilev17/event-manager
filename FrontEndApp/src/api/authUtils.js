import axios from "axios";
import { jwtDecode } from "jwt-decode";

export const api = axios.create({
  baseURL: "/api", // Its just "/api" because we use proxy server in vite.config.js to avoid SOP blocing requests
});

export const storeUserData = (token, username, email) => {
  localStorage.setItem("token", token);
  localStorage.setItem("username", username);
  localStorage.setItem("email", email);
}

export const getToken = () => {
  return localStorage.getItem("token");
};
export const getUsername = () => {
  return localStorage.getItem("username");
};
export const getEmail = () => {
  return localStorage.getItem("email");
};

export const removeUserData = () => {
  localStorage.removeItem("token");
  localStorage.removeItem("username");
  localStorage.removeItem("email");
};

export const isTokenExpired = () => {
  const token = getToken();

  if (!token) {
    return true; // No token, consider it expired
  }

  try {
    const decoded = jwtDecode(token);
    const currentTime = Date.now() / 1000; // Convert to seconds
    if (decoded.exp < currentTime) {
      removeUserData();
      return true; // Token expired
    }
    return false; // Token is valid
  } catch (error) {
    removeUserData();
    return true; // Invalid token format, consider it expired
  }
};

api.interceptors.request.use(
  (config) => {
    const token = getToken();
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export default api; // Export the configured Axios instance
