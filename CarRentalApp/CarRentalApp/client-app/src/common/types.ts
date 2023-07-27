interface Response {
    statusCode: number,
    errorMessage?: string
}

export interface GetAllRentalsResponse extends Response {
    rentalsList: Rental[]
}

export interface CommandResponse extends Response {
    returnedId: number
}

export interface Rental {
    id: number,
    carId: number,
    customerId: number,
    dateFrom: Date,
    dateTo: Date
}