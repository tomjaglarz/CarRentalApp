import { useSignOut, useIsAuthenticated } from 'react-auth-kit';
import { Button } from 'react-bootstrap';
import Container from 'react-bootstrap/Container';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import { useNavigate } from 'react-router-dom';

function NavigationBar() {
  const signOut = useSignOut();
  const authenticated = useIsAuthenticated();
  const navigate = useNavigate();

  const logout = () => {
    signOut();
    console.log("User logged out");
    navigate("\login");
  }

  return (
    <>
      <Navbar bg="dark" data-bs-theme="dark">
        <Container>
          <Navbar.Brand href="/">Car rental app</Navbar.Brand>
          <Nav className="me-auto">
            <Nav.Link href="/rentals">Rentals</Nav.Link>
            <Nav.Link href="/customers">Customers</Nav.Link>
            <Nav.Link href="/cars">Cars</Nav.Link>
          </Nav>
        </Container>{
          (authenticated()) ?
            <Button className='button' onClick={logout}>Log out</Button>
            :
            null
        }
      </Navbar>
    </>
  )
}

export default NavigationBar;