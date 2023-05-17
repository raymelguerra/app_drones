# Musalasoft - Medication Transport Drone Fleet Project

This project is an API REST application that allows clients to communicate with a fleet of drones that transport medications. The application was developed in C# using the .NET Core 6 framework and SQLite as the database. Libraries such as AutoMapper, FluentValidation, and NUnit were also used to improve code quality and testing.

## Features

The application has the following features:

- Load medications onto a drone
- Unload medications from a drone
- Get the list of available drones
- Get information about a specific drone
- Get the event history of a specific drone

Here are some examples of API inputs and outputs:

### Registering a drone

```javascript
POST /api/dispatch/registry
```
</br>

**Input:**

```json
{
  "serialNumber": "SN78440186",
  "model": "Lightweight",
  "weightLimit": 300,
  "batteryCapacity": 100,
  "state": "IDLE"
}
```

**Output:**

```json
{
  "serialNumber": "SN78440186",
  "model": "Lightweight",
  "weightLimit": 300,
  "batteryCapacity": 100,
  "state": "IDLE"
}
```

### Loading a drone with medication items

```javascript
PATCH /api/dispatch/{droneId}/loading
```
</br>

**Input:**

```json
[
  {
    "name": "Paracetamol",
    "weight": 35,
    "code": "COD43092",
    "image": "<Base64 Image>"
  }
]
```

**Output:**

```json
 NotContent(204)
```

### Checking loaded medication items for a given drone

```javascript
GET /api/dispatch/{droneId}/medications
```
</br>

**Input:**

```json
```

**Output:**

```json
[
  {
    "name": "card Research",
    "weight": 4,
    "code": "Q6PLPD",
    "image": "Bogus.DataSets.Images"
  },
  {
    "name": "port Knoll",
    "weight": 2,
    "code": "OVILMQ",
    "image": "Bogus.DataSets.Images"
  },
  {
    "name": "infomediaries Awesome Metal Mouse",
    "weight": 2,
    "code": "YNCP1M",
    "image": "Bogus.DataSets.Images"
  }...
```

### Checking available drones for loading

```javascript
GET /api/dispatch/availables
```
</br>

**Input:**

```json
```

**Output:**

```json
[
  {
    "id": 5,
    "serialNumber": "string",
    "model": "Lightweight",
    "weightLimit": 100,
    "batteryCapacity": 100,
    "state": "IDLE"
  }
]
```

### Check drone battery level for a given drone

```javascript
GET /api/dispatch/{droneId}/batterylevel
```
</br>

**Input:**

```json
```

**Output:**

```json
{
  "batteryCapacity": 362
}
```

## Functional Requirements

The application meets the following functional requirements:

- Prevent the drone from being loaded with more weight than it can carry
- Prevent the drone from being in LOADING state if the battery level is below 25%
- Introduce a periodic task to check drones battery levels and create history/audit event log for this.

## Non-Functional Requirements

The application meets the following non-functional requirements:

- Input/output data must be in JSON format
- Your project must be buildable and runnable
- Your project must have a README file with build/run/test instructions (use DB that can be run locally, e.g. in-memory, via container)
- Any data required by the application to run (e.g. reference tables, dummy data) must be preloaded in the database.
- JUnit tests are optional but advisable (if you have time)
- Advice: Show us how you work through your commit history.

## Installation

To install and run the application, follow these steps:

1. Clone the repository on your local machine.
2. Open the project in Visual Studio or your preferred code editor.
3. Run the `dotnet restore` command to restore the project dependencies.
4. Run the following command to build and run the application, and execute Entity Framework Core migrations for all projects in the solution:

```javascript
/* 
Run database migrations
  1. Select core project.
  2. Execute migrate command
*/
cd AppDrones.Core
dotnet ef database update
// Run Api project
dotnet run --project AppDrones.Api
// Run Battery Level Checker background service
dotnet run --project AppDrones.BatteryLevelTester
```

## Commit history

commit 8a78b6871891da57dac5cc51d93a0f3e67903c07
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 18:32:11 2023 -0400

    Refactoring code

commit 66d9c56490b17f9ed33af6b6df474e97672be142
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 18:31:50 2023 -0400

    Fix error validation

commit 0e75d04b9bd8faeb183926e8cceb2f66f55d2aef
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 15:59:00 2023 -0400

    add test project

commit 4c80ada73baa2276c5623b8bc5c81212a56b61b6
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 13:26:05 2023 -0400

    Add unique contrain to serial number of the drone

commit 882314fbb90712054a3c8c3ceb276ae452150479
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 12:39:15 2023 -0400

    Add Functional requirements: Introduce a periodic task to check drones battery levels and create history/audit event log for this.

commit b351a907a2e4b6b5e7e4d44e00c6f03ea13de1c9
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 06:14:03 2023 -0400

    Add Fixture: check drone battery level for a given drone

commit 6a8b78cef0de6e03755e64fa33eb8fd8be49b7a7
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 06:02:21 2023 -0400

    Add Fixture: checking available drones for loading;

commit cb26d2fb954def8a43691d6afede78427d33bfa7
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 05:48:18 2023 -0400

    Add Fixture: checking loaded medication items for a given drone;

commit 928d7e66ae166d10061cf62c0b9de286a8b61377
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 05:12:53 2023 -0400

    Add load image to medication. This image is in base64.

commit 0ae40f9ccac4432573a7d6f639b6213b3f661a65
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 04:58:48 2023 -0400

    Load medication without image

commit a43fe6268af98c9e7a852f47d071caba728f6c73
Author: raymelguerra <raymelguerra@gmail.com>
Date:   Tue May 16 02:10:34 2023 -0400

    Initial commit.
    1. Project scaffold
    2. registry drone

commit af0ac842b62e6d1fdbe18386ad503cdc11695efd
Author: Raymel Ramos <108881451+raymelguerra@users.noreply.github.com>
Date:   Fri May 12 22:01:44 2023 -0400

    Update README.md

commit 933a405cc41cdecf71f1602a2e3023e9749acd8a
Author: Raymel Ramos <108881451+raymelguerra@users.noreply.github.com>
Date:   Fri May 12 21:59:35 2023 -0400

    Initial commit


## License

This project is licensed under the MIT License. See the LICENSE file for more information.

## Author

This project was developed by **Raymel Ramos Guerra**, a software developer with over 6 years of experience.
