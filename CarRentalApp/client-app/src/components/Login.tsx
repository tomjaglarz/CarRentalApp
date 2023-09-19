import React, { useState } from 'react';
import { useSignIn } from 'react-auth-kit';
import { Form } from 'react-bootstrap';
import { useForm } from 'react-hook-form';
import { LoginData, LoginResponse } from '../common/types';
import { useNavigate } from 'react-router-dom';
import * as identityApi from "../api/identity/api"

export default function Login() {
    const signIn = useSignIn();
    const navigate = useNavigate();

    const form = useForm<LoginData>();
    const { register, handleSubmit, formState } = form;
    const [loginError, setLoginError] = useState('')
    const { errors } = formState;

    const onSubmit = (data: LoginData) => {

        console.log("Logging in...");
        console.log(data);
        identityApi.LogIn(data)
            .then(response => {
                localStorage.setItem("key", response.queryResult.token);
                signIn({
                    token: localStorage.getItem("key")!,
                    expiresIn: 3600,
                    tokenType: "Bearer",
                    authState: { email: data.email }
                });
                navigate("/rentals");
            })
            .catch(error => {
                console.log(error.response.data.errorMessage);
                setLoginError(error.response.data.errorMessage);
            });
    }

    return (
        <>
            <Form id="login" onSubmit={handleSubmit(onSubmit)} className='col-lg-2 offset-lg-5' noValidate>
                <Form.Group className="formElement">
                    <Form.Label htmlFor="email">Email</Form.Label>
                    <Form.Control
                        id="email"
                        {...register("email", {
                            required: "Email is required"
                        })}>
                    </Form.Control>
                    <p className="error">{errors.email?.message}</p>

                    <Form.Label htmlFor="password">Password</Form.Label>
                    <Form.Control
                        type='password'
                        id="password"
                        {...register("password", {
                            required: "Password is required"
                        })}>
                    </Form.Control>
                    <p className="error">{errors.password?.message}</p>
                </Form.Group>
                <button className="btn btn-primary button" form="login" type="submit">Log in</button>
                <p className="error">{loginError}</p>
            </Form>
        </>
    )
}