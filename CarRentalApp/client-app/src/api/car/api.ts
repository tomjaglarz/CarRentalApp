import * as types from '../../common/types';
import axios from 'axios';
import jwtDecode from 'jwt-decode';
import Cookies from 'universal-cookie';

const cookies = new Cookies();
const jwtToken = cookies.get('_auth');

const instance = axios.create({
    baseURL: 'https://localhost:7173/api/Car/',
    timeout: 30000,
    headers:{
        "Authorization": `Bearer ${jwtToken}`
    }
  });


export async function GetAllCars() : Promise<types.Car[]>{

    try{
        //await new Promise(f => setTimeout(f, 5000));
        const response = await instance.get<types.GetAllCarsResponse>('/GetAllCars');
        return response.data.queryResult.carsList;
    }
    catch (error) {
        throw new Error('Error occured.');
    }
}

export async function PostCar(car: types.Car) : Promise<number>{
    try{
        const response = await instance.post<types.CommandResponse>('', car);
        console.log(`Created car with id:${response.data.returnedId}`);
        return response.data.returnedId;
    }
    catch (error) {
        throw new Error('Error occured.');
    }
}
