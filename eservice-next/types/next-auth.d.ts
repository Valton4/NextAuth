import { user } from "./interfaces";

declare module "next-auth" {
    interface Session {
        user: user.user;
        token: user.token
    }
}