import React, { useState, useEffect } from "react";
import * as rentalApi from '../api/rental/api';
import * as customerApi from '../api/customer/api';
import '../common/types';
import '../assets/App.css';
import { Customer, Rental } from "../common/types";
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { Form } from 'react-bootstrap';

function AddNewRental() {

    const [customersOptions, setCustomersOptions] = useState<Customer[]>([]);
    const [newRental, setNewRental] = useState<Rental>({} as Rental);

    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    useEffect(() => {
        customerApi.GetAllCustomers()
            .then(customers => setCustomersOptions(customers))
            .catch(error => console.log(error));
    }, []);

    useEffect(() =>{
        console.log(newRental);
    })

  
    function NewRental(){
        rentalApi.PostRental(newRental)
            .catch(error => console.log(error));
    };

    return(
        <>
            <Button variant="primary" onClick={handleShow}>
               + Add new rental
            </Button>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>New rental</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form id="addrental" onSubmit={(e) =>{
                            NewRental();
                        }}>
                        <Form.Group className="formElement">
                            <Form.Label htmlFor="customerId">Customer</Form.Label>
                            <Form.Select name="customerId" id="customerId" value={newRental.customerId || '' } onChange={e => setNewRental({...newRental, customerId: parseInt(e.target.value) })}>
                                <option disabled value=''>Select customer...</option>
                                {customersOptions.map(customer => (
                                    <option key={customer.id} value={customer.id}>
                                            {customer.firstName} {customer.lastName}
                                    </option>
                                ))}
                        </Form.Select>
                        </Form.Group>    
                        
                        <Form.Group className="formElement">
                            <Form.Label htmlFor="carId">Car</Form.Label>
                            <Form.Control name="carId" id="carId" type="text" value={newRental.carId || ''} onChange={e => setNewRental({...newRental, carId: parseInt(e.target.value) })}/>
                        </Form.Group>
                        <Form.Group className="formElement">
                            <div className="row">
                                <div className="col">
                                    <Form.Label htmlFor="dateFrom">Date from</Form.Label>
                                    <Form.Control name="dateFrom" id="dateFrom" type="date" value={newRental.dateFrom?.toISOString().slice(0, 10) || ''} onChange={e => setNewRental({...newRental, dateFrom: new Date(e.target.value) })}/>
                                </div>
                                <div className="col">
                                    <Form.Label htmlFor="dateTo">Date to</Form.Label>
                                    <Form.Control name="dateTo" id="dateTo" type="date" value={newRental.dateTo?.toISOString().slice(0, 10) || ''} onChange={e => setNewRental({...newRental, dateTo: new Date(e.target.value) })}/>        
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