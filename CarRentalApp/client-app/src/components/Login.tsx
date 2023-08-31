import React from 'react';
import { useSignIn } from 'react-auth-kit';

export default function Login() {
    const signIn = useSignIn();
    
    const onSubmit = (e: any) => {
        e.preventDefault();
        console.log("Logging in...");
        signIn({
            token: "test",
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