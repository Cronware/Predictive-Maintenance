# Predictive-Maintenance

A **C# WinForms application** that monitors **IoT sensor data** (temperature, vibration, and current), detects anomalies, and predicts failures using **ML.NET** and **MongoDB**.

## 📌 Features
✅ Real-time sensor data visualization  
✅ Anomaly detection & alerts  
✅ ML-based failure prediction  
✅ MongoDB storage & historical data  
✅ Web API (ASP.NET Core) for remote access  
✅ CSV export & reporting  

---

![image](https://github.com/user-attachments/assets/10d5352d-565f-4e82-8a32-757702560fdf)

![image](https://github.com/user-attachments/assets/2b9e34a8-1b09-4da8-a050-e25e31cfd122)

![image](https://github.com/user-attachments/assets/96d4ba39-3c65-4ce5-a242-cff07eaf9794)

## 🛠️ Installation & Setup

### **1️⃣ Install Required Dependencies**
- **[Visual Studio 2022+](https://visualstudio.microsoft.com/downloads/)**
- **[MongoDB Community Server](https://www.mongodb.com/try/download/community)**
- **[.NET 6+ SDK](https://dotnet.microsoft.com/en-us/download)**
- **NuGet Packages:**
  ```sh
  dotnet add package MongoDB.Driver
  dotnet add package Microsoft.ML
  dotnet add package Microsoft.ML.FastTree
  dotnet add package LiveCharts.WinForms
  ```
### **2️⃣ Clone the Repository
```sh
git clone https://github.com/YOUR_USERNAME/IoT-Predictive-Maintenance.git
cd IoT-Predictive-Maintenance
```
### **3️⃣ Set Up MongoDB
1. Start MongoDB Server (default port 27017):
```sh
mongod
```
2. Create the database:
```sh
mongosh
use IoTPredictiveMaintenance
```
### **4️⃣ Run the Application
1. Open Visual Studio and load the solution (.sln).
2. Run the project (F5).
