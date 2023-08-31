import { useState } from "react";
import { Button, Form, Modal } from "react-bootstrap";
import { useForm } from "react-hook-form";
import { Car } from "../common/types";
import * as carApi from "../api/car/api"

interface Props{
    setToUpdate: React.Dispatch<React.SetStateAction<boolean>>
}

function AddNewCar({ setToUpdate } : Props) {

    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    const form = useForm<Car>();
    const { register, handleSubmit, formState } = form;
    const { errors } = formState;

    const [seatsCount, setSeatsCount] = useState(5);

    const changeSeatsCount = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSeatsCount(event.target.valueAsNumber);
    };


    const onSubmit = (data: Car) => {
        console.log('Posting new car', data);

        carApi.PostCar(data)
            .catch(error => console.log(error))
            .finally(() => {
                handleClose();
                setToUpdate(prevState => !prevState);
            });
    };

    return (
        <>
            <Button variant="success" className="button align-items-center justify-content-center" onClick={handleShow}>
                + Add new car
            </Button>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title>New car</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form id="addcar" onSubmit={handleSubmit(onSubmit)} noValidate>
                        <Form.Group className="formElement">
                            <Form.Label htmlFor="carBrand">Car brand</Form.Label>
                            <Form.Control
                                id="carBrand"
                                placeholder="Input car brand..."
                                {...register("brand", {
                                    required: "Brand is required"
                                })}>
                            </Form.Control>
                            <p className="error">{errors.brand?.message}</p>

                            <Form.Label htmlFor="carModel">Car model</Form.Label>
                            <Form.Control
                                id="carModel"
                                placeholder="Input car model..."
                                {...register("modelName", {
                                    required: "Brand is required"
                                })}>
                            </Form.Control>
                            <p className="error">{errors.modelName?.message}</p>

                            <Form.Label htmlFor="seatCount">Seats count: {seatsCount}</Form.Label>
                            <Form.Range
                                id="seatCount"
                                min={2}
                                max={7}
                                defaultValue={seatsCount}
                                {...register("seatsCount", {
                                    onChange: changeSeatsCount
                                })}>
                            </Form.Range>
                            <p className="error">{errors.seatsCount?.message}</p>

                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <button className="btn btn-primary" form="addcar" type="submit">Add</button>

                </Modal.Footer>
            </Modal>
        </>
    )
}

export default AddNewCar