import * as types from '../../common/types';
import axios from 'axios';

const instance = axios.create({
    baseURL: `${process.env.REACT_APP_API_URL}/Identity/`,
    timeout: 30000,
});

export async function LogIn(loginData: types.LoginData): Promise<types.LoginResponse> {

    const response = await instance.post('/login', loginData, {
        headers: {
            "Content-Type": "application/json"
        }
    });
    return response.data;
}