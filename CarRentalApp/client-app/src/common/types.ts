interface Response {
    statusCode: number,
    errorMessage?: string
}

interface RentalsList{
    rentalsList: Rental[]
}

interface CustomersList{
    customersList: Customer[]
}

interface CarsList{
    carsList: Car[]
}

interface LoginResult{

    token:string,
    isAuthorized: boolean,
    message: string
}

export interface GetAllRentalsResponse extends Response {
    queryResult: RentalsList
}

export interface GetAllCustomersResponse extends Response {
    queryResult: CustomersList
}

export interface GetAllCarsResponse extends Response {
    queryResult: CarsList
}

export interface LoginResponse extends Response {
    queryResult: LoginResult
}

export interface CommandResponse extends Response {
    returnedId: number
}

export interface Rental {
    id: number,
    carId: number,
    customerId: number,
    dateFrom: Date,
    dateTo: Date,
}

export interface Customer{
    id: number,
    firstName: string,
    lastName: string,
    email: string,
    adress: string
}

export interface Car{
    id: number,
    brand: string,
    modelName: string,
    seatsCount: number
}

export interface LoginData{
    email: string,
    password: string
}
