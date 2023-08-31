import { Button } from "react-bootstrap"
import { Car } from '../common/types';

interface Props{
    car: Car
}

function CarCard({car} : Props) {
    return (
        <div className="card">
            <img className="card-img-top" src="/car-image.jpg" alt="Car"></img>
            <div className="card-body">
                <h5 className="card-title">{car.brand}</h5>
                <h6 className="card-subtitle mb-2 text-muted">{car.modelName}</h6>
                <p className="card-text">Seats count: {car.seatsCount}</p>
                <Button variant="primary" className="button" size="sm">Edit Car</Button>
                <Button variant="danger" className="button" size="sm">Remove Car</Button>
            </div>
        </div>
    )
}
export default CarCard