import React, { useState } from "react";
import * as rentalApi from '../api/rental/api';
import '../common/types';
import '../assets/App.css';
import { Customer, Rental, Car } from "../common/types";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { Form } from 'react-bootstrap';
import { useForm } from "react-hook-form";

interface Props {
    customers: Customer[],
    cars: Car[],
    setUpdateList: React.Dispatch<React.SetStateAction<boolean>>
}

function AddNewRental({ customers, cars, setUpdateList } : Props) {

    const [minDateTo, setMinDateTo] = useState<string>("");

    const form = useForm<Rental>();
    const { register, handleSubmit, formState, getValues } = form;
    const { errors } = formState;

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const onSubmit = (data: Rental) => {
        console.log('Posting new rental', data);
        rentalApi.PostRental(data)
            .catch(error => console.log(error))
            .finally(() => {
                handleClose();
                setUpdateList(prevState => !prevState);
            });
    };

    return(
        <>
            <Button variant="success" className="button" onClick={handleShow}>
               + Add new rental
            </Button>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>New rental</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form id="addrental" onSubmit={handleSubmit(onSubmit)} noValidate>
                        <Form.Group className="formElement">
                            <Form.Label htmlFor="customerId">Customer</Form.Label>
                            <Form.Select 
                                id="customerId" 
                                defaultValue=''
                                {...register("customerId", {
                                    required: "Customer is required"
                                })}>
                                    <option disabled value=''>Select customer...</option>
                                    {customers.map(customer => (
                                    <option key={customer.id} value={customer.id}>
                                            {customer.firstName} {customer.lastName}
                                    </option>
                                ))}
                            </Form.Select>
                            <p className="error">{errors.customerId?.message}</p>
                        </Form.Group>    
                        
                        <Form.Group className="formElement">
                            <Form.Label htmlFor="carId">Car</Form.Label>
                            <Form.Select 
                                id="carId"
                                defaultValue=''
                                {...register("carId", {
                                    required : "Car is required"
                                })}>
                                    <option disabled value=''>Select car...</option>
                                    {cars.map(car => (
                                    <option key={car.id} value={car.id}>
                                           (#{car.id}) {car.brand} {car.modelName}
                                    </option>
                                ))}
                            </Form.Select>
                           
                            <p className="error">{errors.carId?.message}</p>
                        </Form.Group>

                        <Form.Group className="formElement">
                            <div className="row">
                                <div className="col">
                                    <Form.Label htmlFor="dateFrom">Date from</Form.Label>
                                    <Form.Control 
                                        id="dateFrom"
                                        type="date" 
                                        min={new Date().toISOString().substring(0, 10)}
                                        {...register("dateFrom", {
                                            required: "Rental start date is required"
                                        })}
                                        onChange={e => setMinDateTo(e.target.value)}
                                    />
                                    <p className="error">{errors.dateFrom?.message}</p>
                                </div>
                                <div className="col">
                                    <Form.Label htmlFor="dateTo">Date to</Form.Label>
                                    <Form.Control 
                                        id="dateTo"
                                        type="date"
                                        min={minDateTo}
                                        {...register("dateTo", {
                                            validate: (fieldValue) => {
                                                const dateFromValue = getValues("dateFrom");
                                                if (dateFromValue <= fieldValue)
                                                    return true;
                                                return "The rental end date cannot be earlier than the start date";
                                            }
                                        })} 
                                    /> 
                                    <p className="error">{errors.dateTo?.message}</p>       
                                </div>
                            </div>
                            
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <button className="btn btn-primary" form="addrental" type="submit">Add</button>
                    
                </Modal.Footer>
                
            </Modal>
            
            </>
    );
}

export default AddNewRental;