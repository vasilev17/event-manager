/** @type {import('tailwindcss').Config} */

export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors:{
        primary : "#40ddc7",
        primary2 : "#5abab7",
        secondary : "#29005d",
        secondary2 : "#cec2dd",
      }

    },
  },
  plugins: [],
};
