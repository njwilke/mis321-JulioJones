const url = "http://localhost:5248/api/Car"
let myCars = []

async function handleOnLoad() {
    let html = `
    <div class="jumbotron"><h1 style = "font-family: monospace">Welcome to the Julio Jones Kia Dealership!</h1></div>
    <img class="center" src="Assets/JulioJones.png" alt="Julio" height="200"/>
    
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <h2 style="font-family: monospace; text-align: center;">Work-Outs</h2>
                <div id="tableBody"></div>
            </div>
            <div class="col-md-6">
                <h2 style="font-family: monospace;">Add a new car</h2>
                <form onsubmit = "return false">
                    <label for="carMakeModel">Make and Model:</label><br>
                    <input type="text" id="carMakeModel" name="carMakeModel"><br>
                    <label for="mileage">Mileage:</label><br>
                    <input type="text" id="mileage" name="mileage"><br>
                    <label for="dateEntered">Date:</label><br>
                    <input type="date" id="dateEntered" name="dateEntered">
                    <button onclick="handleCarAdd()" class="btn btn-primary">Add Car</button>
                </form>
            </div>
        </div>`
    document.getElementById('app').innerHTML = html
    populateTable()
}

async function createCars() {
    let response = await fetch(url, [])
    myCars = await response.json()
    console.log(myCars)
}

async function populateTable() {
    await createCars()
    myCars.sort(function(a, b) {
        return new Date(b.date) - new Date(a.date)
    })
    let html = `<table class="table table-striped">
    <tr>
        <th>Make & Model</th>
        <th>Mileage</th>
        <th>Date Entered in Inventory</th>
        <th>Hold</th>
        <th>Sell</th>
    </tr>`
    myCars.forEach(function(car) {
        if(car.sold != true) {
            html += `
            <tr>
                <td>${car.carMakeModel}</td>
                <td>${car.mileage}</td>
                <td>${car.date}</td>`
            if(car.hold === false) {html += `
                <td><button onclick = "handleCarDelete(${car.carID})" class="btn btn-outline-danger">Sell</button></td>
                <td><button onclick = "handleCarHold(${car.carID})" class="btn btn-outline-secondary">Hold</button></td>
                </tr>`
            }
            else if(car.hold === true) { html += `
                <td><button onclick = "handleCarDelete(${car.carID})" class="btn btn-outline-danger" disabled>Sell</button></td>
                <td><button onclick = "handleCarHold(${car.carID})" class="btn btn-outline-secondary active">Hold</button></td>
                </tr>`
            }
        }
    })
    html += `</table>`
    document.getElementById('tableBody').innerHTML = html
}

async function handleCarAdd() {
    let car = {CarID: 0, CarMakeModel: document.getElementById('carMakeModel').value, Mileage: document.getElementById('mileage').value, Date: document.getElementById('dateEntered').value, Hold: false, Sold: false}
    await saveCar(car)
    populateTable()
    document.getElementById('carMakeModel').value = ''
    document.getElementById('mileage').value = ''
    document.getElementById('dateEntered').value = ''
}

async function saveCar(car) {
    console.log(car)
    await fetch(url, {
        method: "POST",
        body: JSON.stringify(car),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
}

async function handleCarHold(searchID) {
    myCars.forEach(function(car) {
        if(car.carID === searchID) {
            if(car.hold === false) {
                car.hold = true
            } else if(car.hold === true) {
                car.hold = false
            }
            updateCarHold(car, searchID)
        }
    })
    await createCars()
    populateTable()
}

async function updateCarHold(car, carID) {
    await fetch(url + '/' + carID, {
        method: "PUT",
        body: JSON.stringify(car),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
}

async function handleCarDelete(searchID) {
    myCars.forEach(function(car) {
        if(car.carID === searchID) {
            car.sold = true
            updateCarSold(car)
        }
    })
    await createCars()
    populateTable()
}

async function updateCarSold(car) {
    await fetch(url, {
        method: "PUT",
        body: JSON.stringify(car), 
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
}
