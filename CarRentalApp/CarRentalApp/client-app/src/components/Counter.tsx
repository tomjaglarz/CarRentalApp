import React from 'react';
import { useState } from 'react';
import '../assets/App.css';

function Counter() {
    const [counter, setCounter] = useState(0);

    const handlePlusClick = () => {
      setCounter((state: number) => state + 1); 
    }
    
    const handleMinusClick = () => {
      setCounter((state: number) => state -1);
    }
    

    return (
    <div className="App">
      <header className="App-header">
        <h2>{counter}</h2>
        <button onClick={handlePlusClick}>+</button>
        <button onClick={handleMinusClick}>-</button>
      </header>
    </div>
  );
}

export default Counter;
