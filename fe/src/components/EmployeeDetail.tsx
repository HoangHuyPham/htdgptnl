import { IUser } from "@interfaces"

export const EmployeeDetail: React.FC<{ user: IUser }> = ({ user }) => {
    return <span>
        <div className="bg-white px-5 text-4xl font-semibold">Employee Detail</div>

        <div className="flex justify-center gap-5 p-5 bg-white">
            <div className="flex w-[100%] px-5 gap-5 justify-between">
                <div className="flex flex-col">
                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Code:</span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.detail?.code} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Department:</span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.detail?.department?.name} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Grade:</span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.detail?.grade?.name} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Group:</span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.detail?.group?.name} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Operation:</span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.detail?.operation?.name} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Position E:</span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.detail?.positionE?.name} />
                    </span>

                    <span className="flex text-2xl w-[420px] justify-between">
                        <span>Process:</span>
                        <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.detail?.process?.name} />
                    </span>
                </div>

                <div className="flex flex-col">
                    {
                        user?.employee?.supervisor && (<span className="flex text-2xl w-[420px] justify-between">
                            <span className="text-red-500 font-bold">Supervisor: </span>
                            <input disabled className="w-[280px] font-semibold" type="text" value={user?.employee?.supervisor?.detail?.fullName} />
                        </span>)
                    }
                </div>

            </div>
        </div>
    </span>
}