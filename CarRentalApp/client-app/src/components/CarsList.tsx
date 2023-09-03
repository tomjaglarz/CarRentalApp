import CarCard from './CarCard'
import { ChangeEvent, useEffect, useState } from 'react';
import { Car } from '../common/types';
import * as carApi from '../api/car/api';
import AddNewCar from './AddNewCar';
import { trackPromise, usePromiseTracker } from 'react-promise-tracker';
import { PulseLoader } from 'react-spinners';

function CarsList() {
    const [cars, setCars] = useState<Car[]>([]);
    const [filteredCars, setFilteredCars] = useState<Car[]>([]);
    const [toUpdate, setToUpdate] = useState(false);
    const { promiseInProgress } = usePromiseTracker();

    useEffect(() => {
        GetCars();
    }, [toUpdate])

    function GetCars() {
        trackPromise(
            carApi.GetAllCars()
                .then(cars => {
                    setCars(cars);
                    setFilteredCars(cars)
                })
                .catch(error => console.log(error))
        );
    }

    const FilterCars = (event: ChangeEvent<HTMLInputElement>) => {
        let filterQuery = event.target.value.toLowerCase();
        if(filterQuery){
            setFilteredCars(cars.filter(car => car.brand.toLowerCase().includes(filterQuery)));
        }else{
            setFilteredCars(cars);
        }
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
                        <input type="search" id="form1" className="form-control" placeholder='Search...' onChange={FilterCars}/>

                    </div>
                </div>
            </div>
            <div className="row table">
                {filteredCars.map(car => (
                    <div key={car.id} className="col-sm-4">
                        <CarCard car={car}></CarCard>
                    </div>
                ))}
            </div>
        </>
    )
}

export default CarsList;