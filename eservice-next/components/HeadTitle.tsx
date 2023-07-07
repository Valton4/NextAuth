export const HeadTitle = ({ title, description }: any) => {
    return (
        <div className="header container m-auto bg-gray-100 shadow-2xl">
            <h1 className=" text-start text-3xl text-red-700">{title}</h1>
            <p className="text-gray-300">{description}</p>
        </div>
    )
}