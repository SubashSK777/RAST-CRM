# RAST-CRM

Remote Asset Sensor Tracking (RAST) CRM - Monitoring and Management System.

## Overview
RAST-CRM is a powerful monitoring and management system built with ASP.NET. It is designed for tracking remote asset sensors, providing real-time data visualization, alert management, and comprehensive reporting. This system is ideal for organizations that need to monitor device health, battery levels, and sensor alerts across multiple sites.

## Key Features
- **Interactive Dashboard**: A centralized view of all active sensors, alerts, and system status.
- **Sensor Management**: Comprehensive tools to configure, reposition, and monitor sensors.
- **Alert & Notification System**: 
  - Real-time sensor alert tracking.
  - SMS alert logging.
  - Historical alert reports.
- **Hierarchical Management**:
  - **Organization Management**: Manage multiple organizations.
  - **Site Management**: Track sensors across different site locations with map integration.
  - **User Management**: Role-based access control (RBAC) and user administration.
- **Device Health Monitoring**:
  - Battery status tracking for all remote devices.
  - Hub and trigger status monitoring.
- **Map Integration**: Visualize sensor distributions using OpenLayers.
- **Reporting**: Generate detailed reports for alerts, sensor management, and site activity.

## Tech Stack
- **Frontend**: ASP.NET Web Forms, Bootstrap, jQuery, OpenLayers, jsPDF.
- **Backend**: C# (.NET Framework), Data Access Layer (DAL) pattern.
- **Database**: SQL Server.
- **Tools**: Visual Studio, NuGet.

## Project Structure
- **/RAST**: The main web application project containing ASPX pages, styles, and scripts.
- **/RAST.DAL**: Data Access Layer containing business logic and database interactions.
- **/RAST.Utilities**: Core utility classes and shared helper functions.
- **/snda_rast**: Contains the database schema and SQL dumps (`snda_rast.sql`).

## Getting Started

### Prerequisites
- Visual Studio (2019 or later recommended)
- SQL Server
- .NET Framework

### Installation
1. **Clone the repository**:
   ```bash
   git clone https://github.com/SubashSK777/RAST-CRM.git
   ```
2. **Database Setup**:
   - Create a new database in SQL Server.
   - Import the data from `snda_rast/snda_rast.sql`.
3. **Configuration**:
   - Open the solution in Visual Studio.
   - Update the connection string in `RAST/Web.config` to point to your SQL Server instance.
4. **Run the project**:
   - Build the solution.
   - Run `RAST` using IIS Express or your preferred web server.

## License
[Insert License Information Here]

---
*Developed for robust asset tracking and management.*
