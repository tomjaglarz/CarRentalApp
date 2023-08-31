import * as types from '../../common/types';
import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7173/api/Rental/',
    timeout: 30000,
});

export async function GetAllRentals() : Promise<types.Rental[]>{

    try{
        //await new Promise(f => setTimeout(f, 4000));
        const response = await instance.get<types.GetAllRentalsResponse>('/GetAllRentals');
        return response.data.queryResult.rentalsList;
    }
    catch (error) {
        throw new Error('Error occured.');
    }
}

export async function PostRental(rental: types.Rental) : Promise<number>{
    try{
        const response = await instance.post<types.CommandResponse>('', rental);
        console.log(`Created rental with id:${response.data.returnedId}`);
        return response.data.returnedId;
    }
    catch (error) {
        throw new Error('Error occured.');
    }
}

export async function DeleteRental(rentalId : Number) : Promise<number>{
    try{
        const response = await instance.delete<types.CommandResponse>(`/Delete?id=${rentalId}`);
        console.log(`Deleted rental with id:${response.data.returnedId}`);
        return response.data.returnedId;
    }
    catch (error) {
        throw new Error('Error occured.');
    }
}
