import * as types from '../../common/types';
import axios from 'axios';

const instance = axios.create({
    baseURL: 'https://localhost:7173/api/Customer/',
    timeout: 30000,
  });

export async function GetAllCustomers() : Promise<types.Customer[]>{

    try{
        const response = await instance.get<types.GetAllCustomersResponse>('/GetAllCustomers');
        return response.data.queryResult.customersList;
    }
    catch (error) {
        throw new Error('Error occured.');
    }
}
