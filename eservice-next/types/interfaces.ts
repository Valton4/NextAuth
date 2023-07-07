export interface user {
    name?: string | null | undefined,
    role?: string;
    userName?: string
    accessToken?: string,
    token?: string,
    credentials?: string
}


export interface loggedInUser {
    id: string,
    UserName:string,
    Email:string,
    Password:string
}