import NavigationBar from "./NavigationBar";
import RentalList from "./RentalsList";
import {Route, Routes} from "react-router-dom";

function App(){
    return(
        <>
            <NavigationBar></NavigationBar>
            <div className="containter">
                <Routes>
                    <Route path="/rentals" element={<RentalList></RentalList>}></Route>
                    <Route path="/" element={<RentalList></RentalList>}></Route>
                </Routes>
            </div>
        </>
    )
}

export default App
