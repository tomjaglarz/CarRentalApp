import React from 'react';
import { useSignIn } from 'react-auth-kit';
import axios from 'axios';

export default function Login() {
    const signIn = useSignIn();
    
    const onSubmit = (e: any) => {

        e.preventDefault();
        console.log("Logging in...");
        axios.post("https://localhost:7173/api/Identity/token", "mytoken", 
        {headers: {"Content-Type": "application/json"}})
            .then(response => localStorage.setItem("key", response.data));
        signIn({
            token: localStorage.getItem("key")!,
            expiresIn: 3600,
            tokenType: "Bearer",
        });
    }

    return (
        <form onSubmit={(e) =>onSubmit(e)}> 
            <label>
                <p>Username</p>
                <input type="text" />
            </label>
            <label>
                <p>Password</p>
                <input type="password" />
            </label>
            <div>
                <button type="submit">Submit</button>
            </div>
        </form>
    )
}