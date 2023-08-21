import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';

function NavigationBar(){
    return(
        <>
          <Navbar bg="dark" data-bs-theme="dark">
            <Container>
              <Navbar.Brand href="/">Car rental app</Navbar.Brand>
              <Nav className="me-auto">
                <Nav.Link href="/rentals">Rentals</Nav.Link>
                <Nav.Link href="/customers">Customers</Nav.Link>
                <Nav.Link href="/cars">Cars</Nav.Link>
              </Nav>
            </Container>
          </Navbar>
        </>
    )
}

export default NavigationBar;