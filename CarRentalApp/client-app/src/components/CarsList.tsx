import CarCard from './CarCard'
import { useEffect, useState } from 'react';
import { Car } from '../common/types';
import * as carApi from '../api/car/api';
import AddNewCar from './AddNewCar';
import { trackPromise, usePromiseTracker } from 'react-promise-tracker';
import { PulseLoader } from 'react-spinners';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

function CarsList() {
    const [cars, setCars] = useState<Car[]>([]);
    const [toUpdate, setToUpdate] = useState(false);
    const { promiseInProgress } = usePromiseTracker();

    useEffect(() => {
        GetCars();
    }, [toUpdate])

    function GetCars() {
        trackPromise(
            carApi.GetAllCars()
                .then(cars => setCars(cars))
                .catch(error => console.log(error))
        );
    }

    return (
        <>
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
            <div hidden={promiseInProgress} style={{ marginTop: "1%", marginLeft: "1%" }}>
                <AddNewCar setToUpdate={setToUpdate}></AddNewCar>
                <div className="input-group">
                    <div className="form-outline">
                        <input type="search" id="form1" className="form-control" placeholder='Search...' />

                    </div>
                    <button type="button" className="btn btn-primary">
                    <i className="fa fa-search"></i>
                    </button>
                </div>
            </div>
            <div className="row table">
                {cars.map(car => (
                    <div key={car.id} className="col-sm-4">
                        <CarCard car={car}></CarCard>
                    </div>
                ))}
            </div>
        </>
    )
}

export default CarsList;