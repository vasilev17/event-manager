
<div align="center">
  
<h1>🎟🗃 Event Manager — Events & Activities</h1>
  
<p>A Full-Stack Web Application for discovering, creating and attending events and activities.<br> 
It ships with an ASP.NET Core REST API Backend, a React Front-End styled with Tailwind CSS, and MySQL Data Persistence.</p>

<img src="https://img.shields.io/badge/Team%20-%20Project%20-%20gray?logo=codecrafters&labelColor=orange" style="height: 30px; width: auto;">

</div>

---

## ✨ Key Features

- **Browse & Search** - Explore and filter events by ***name, price bracket, date range, address, user and/or category***.

- **Engage with Postings** - Set and change your **attendance status**, as well as **give ratings** to events/activities.

- **Organizer Tools (Create / Delete)** - Share and let other people know and attend events you are organizing by ***uploading images, sharing ticket types and prices and listing important information***.

- **Ticket Operations** - ***Create, remove and book*** tickets.

- **Email Notifications** - Recieve personal account management **confirmation emails**.

---

## 🏗️ Tech Stack

| Layer | Technology |
|-------|------------|
|**Front-End**|React, Vite, React-Router, Axios|
|**Styling**|Tailwind CSS|
|**Backend**|ASP.NET Core Web API, Entity Framework Core, MySQL, Cloudinary|
|**Security**|JWT Bearer Tokens|
|**E-mail**|[SendGrid REST API](https://sendgrid.com)|

---

## 🚀 Getting Started (Development)

### Clone
```bash
git clone https://github.com/vasilev17/event-manager.git
```

### Backend Setup
1.  Create a `appsettings.Development.json` file
2.	Copy the connection string part from `appsettings.json` into it
3.	Enter the missing values from the connection string for your MySQL server installation
4.	Run `Update-Database` command to update your database
5.  Set up JWT    
    5.1 Copy the "Jwt" section from `appsettings.json` to `appsettings.Develoment.json`    
    5.2 Generate a signing key and put it in your `appsettings.Development.json`    
    5.3 Put token duration in time span format    
    5.4 Issuer and audience are the localhost addresses of the back-end and front-end
6.  Set up the email sender    
    6.1 Go to [Emails with C# & SendGrid API](https://www.twilio.com/en-us/blog/send-emails-using-the-sendgrid-api-with-dotnetnet-6-and-csharp) and follow the steps for creating an API key    
    6.2 Copy the "EmailSender" section from `appsettings.json` to `appsettings.Develoment.json`    
    6.3 Ener the needed keys.
    
### Front-End Setup
```bash
cd event-manager/FrontEndApp
npm install
npm run dev
```

---

## 🎬 Showcase

### Backend API
<img width="1000" height="600" alt="Event Manager API Postman" src="https://github.com/user-attachments/assets/39f0f045-bdbe-4275-9592-7a011da80609" />

### Front-End
<img width="1000" height="600" alt="Event Manager FrontEnd" src="https://github.com/user-attachments/assets/ff88874e-72e8-46c2-8b65-6f46d1e285e6" />

---

## 🧪 Testing
Unit and Integration tests are available in the `/EventManager/EventManager.Tests` directory
