import * as types from '../../common/types';
import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7173/api/Identity/',
    timeout: 30000,
});

export async function GetAllCars(): Promise<types.Car[]> {

    try {
        //await new Promise(f => setTimeout(f, 5000));
        const response = await instance.get<types.GetAllCarsResponse>('/GetAllCars');
        return response.data.queryResult.carsList;
    }
    catch (error) {
        throw new Error('Error occured.');
    }
} 

export async function LogIn(loginData: types.LoginData): Promise<types.LoginResponse> {
    
        const response = await instance.post('/login', loginData, {
            headers: {
                "Content-Type": "application/json"
            }
        });
        return response.data;
}

export async function PostCar(car: types.Car): Promise<number> {
    try {
        const response = await instance.post<types.CommandResponse>('', car);
        console.log(`Created car with id:${response.data.returnedId}`);
        return response.data.returnedId;
    }
    catch (error) {
        throw new Error('Error occured.');
    }
}
