import React, { useState, useEffect } from "react";
import '../assets/App.css';
import '../common/types';
import { Rental, GetAllRentalsResponse } from "../common/types";


function RentalList() {
    const [rentals, setRentals] = useState<Rental[]>([]);
    
    useEffect(() => {
        fetch('https://localhost:7173/api/Rental/GetAllRentals')
            .then(response => response.json())
            .then(data => data["queryResult"] as GetAllRentalsResponse)
            .then(rentals => setRentals(rentals.rentalsList));
    }, []);

    
    return (
        <div className="App.css">
            <table>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Car ID</th>
                        <th>Customer ID</th>
                        <th>Date From</th>
                        <th>Date To</th>
                    </tr>
                </thead>
                <tbody>
                    {rentals.map(rental => (
                        <tr key={rental.id}>
                            <td>{rental.id}</td>
                            <td>{rental.carId}</td>
                            <td>{rental.customerId}</td>
                            <td>{rental.dateFrom.toString()}</td>
                            <td>{rental.dateTo.toString()}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
                
        </div>
    );
}

export default RentalList;