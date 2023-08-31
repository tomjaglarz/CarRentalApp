import React, { useState, useEffect } from "react";
import '../common/types';
import Table from 'react-bootstrap/Table';
import * as rentalApi from '../api/rental/api';
import * as customerApi from '../api/customer/api';
import * as carApi from '../api/car/api';

import { Rental, Customer, Car } from "../common/types";
import { Button } from "react-bootstrap";
import { trackPromise, usePromiseTracker } from 'react-promise-tracker';
import AddNewRental from "./AddNewRental";
import { PulseLoader } from "react-spinners";



function RentalList() {
    const [rentals, setRentals] = useState<Rental[]>([]);
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [cars, setCars] = useState<Car[]>([]);
    const [updateList, setUpdateList] = useState(false);

    const { promiseInProgress } = usePromiseTracker();

    useEffect(() => {
            GetRentals();
    },[updateList]);

    useEffect(() => {
        trackPromise(
            customerApi.GetAllCustomers()
                .then(customers => setCustomers(customers))
                .catch(error => console.log(error)))
    }, []);

    useEffect(() => {
        trackPromise(
            carApi.GetAllCars()
                .then(cars => setCars(cars))
                .catch(error => console.log(error)));
    }, []);

    function GetRentals(){
        trackPromise(
            rentalApi.GetAllRentals()
            .then(rentals => setRentals(rentals))
            .catch(error => console.log(error)));
    }

    function RemoveRental(rentalId: number) {
        rentalApi.DeleteRental(rentalId)
            .catch(error => console.log(error))
            .finally(() => setUpdateList(prevState => !prevState));
    }

    function GetCustomerName(customerId: number): string {
        const customer = customers.find(customer => customer.id === customerId)
        if (customer !== undefined)
            return `${customer.firstName} ${customer.lastName}`;
        return "";
    }

    function GetCar(carId: number): string {
        const car = cars.find(car => car.id === carId)
        if (car !== undefined)
            return `(#${car.id}) ${car.brand} ${car.modelName}`;
        return "";
    }

    return (
        <>

            <div className="table">
                <div className="d-flex justify-content-center align-items-center">
                    {
                        (promiseInProgress === true) ?
                            <PulseLoader
                                color="#212529"
                                cssOverride={{
                                    position: 'absolute',
                                    paddingTop: '30%'
                                }}
                            />
                            :
                            null
                    }
                </div>
                <div hidden={promiseInProgress}>
                    <AddNewRental customers={customers} cars={cars} setUpdateList={setUpdateList}></AddNewRental>
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
                                    <td>{GetCar(rental.carId)}</td>
                                    <td>{GetCustomerName(rental.customerId)}</td>
                                    <td>{new Date(rental.dateFrom).toLocaleDateString()}</td>
                                    <td>{new Date(rental.dateTo).toLocaleDateString()}</td>
                                    <td>
                                        <Button variant="primary" className="button" size="sm" disabled>Edit</Button>
                                        <Button variant="danger" className="button" size="sm" onClick={() => RemoveRental(rental.id)}>Remove</Button>
                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                    
                </div>


            </div>
        </>


    );
}

export default RentalList;