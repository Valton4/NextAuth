// export { default } from "next-auth/middleware";
import { withAuth } from "next-auth/middleware";
import { NextRequest, NextResponse } from "next/server";

export default withAuth(

  function middleware(req) {

    if (req.nextUrl.pathname.startsWith('/admin') &&
      req.nextauth.token?.user?.roles?.result.some((role: any) => role !== "Admin")
    ) {
      return new NextResponse("You are not authorized")
    }
    const response = NextResponse.next();
    response.headers.set("Access-Control-Allow-Origin", "*")
    response.headers.set("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE,OPTIONS")
    response.headers.set("Access-Control-Allow-Headers", "Content-Type ,Authorization")
    response.headers.set("Access-Control-Max-Age", "86400")


    return response;


  },
  {
    callbacks: {
      authorized: ({ token }) => !!token,
    },
    secret: process.env.NEXTAUTH_SECRET_KEY
  }
);

export const config = {
  matcher: ['/((?!register|api|login).*)']
  // matcher: ["/admin/:path*", "/user/:path*"],
};