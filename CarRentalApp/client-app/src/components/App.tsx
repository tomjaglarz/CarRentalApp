import { RequireAuth } from "react-auth-kit";
import CarsList from "./CarsList";
import Login from "./Login";
import NavigationBar from "./NavigationBar";
import RentalList from "./RentalsList";
import {Route, Routes} from "react-router-dom";

function App(){
    return(
        <>
            <NavigationBar></NavigationBar>
            <div className="containter"> 
                <Routes>
                    {/* <Route path="/rentals" element={<RentalList></RentalList>}></Route> */}
                    <Route path="/rentals" element={<RequireAuth loginPath="/login"><RentalList></RentalList></RequireAuth>}></Route>
                    <Route path="/cars" element={<CarsList></CarsList>}></Route>
                    <Route path="/login" element={<Login></Login>}></Route>
                </Routes>
            </div>
        </>
    )
}

export default App
