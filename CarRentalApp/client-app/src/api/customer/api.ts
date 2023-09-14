import * as types from '../../common/types';
import axios from 'axios';
import Cookies from 'universal-cookie';

const cookies = new Cookies();
const token = cookies.get('_auth');
const authType = cookies.get('_auth_type')

const instance = axios.create({
    baseURL: 'https://localhost:7173/api/Customer/',
    timeout: 30000,
    headers:{
        "Authorization": `${authType} ${token}`
    }
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
