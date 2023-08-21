import React, { useState, useEffect } from "react";
import '../common/types';
import Table from 'react-bootstrap/Table';
import * as rentalApi from '../api/rental/api';

import { Rental } from "../common/types";
import { Button } from "react-bootstrap";
import AddNewRental from "./AddNewRental";


function RentalList() {
    const [rentals, setRentals] = useState<Rental[]>([]);
    
    useEffect(() => {
        rentalApi.GetAllRentals()
            .then(rentals => setRentals(rentals))
            .catch(error => console.log(error));
    }, []);

   function RemoveRental(rentalId: number){
        rentalApi.DeleteRental(rentalId)
            .then(_ => setRentals(rentals.filter(rental => rental.id != rentalId)))
            .catch(error => console.log(error));
   }
    
    return (
        <><div className="App.css">
        <Table striped hover bordered>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Car</th>
                    <th>Customer</th>
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
                        <td>{new Date(rental.dateFrom).toLocaleDateString()}</td>
                        <td>{new Date(rental.dateTo).toLocaleDateString()}</td>
                        <td>
                            <Button variant="primary" size="sm">Edit</Button>
                            <Button variant="danger" size="sm" onClick={() => RemoveRental(rental.id)}>Remove</Button>
                        </td>
                    </tr>
                ))}
            </tbody>
        </Table>
    </div>
    <AddNewRental></AddNewRental></>
        
    );
}

export default RentalList;