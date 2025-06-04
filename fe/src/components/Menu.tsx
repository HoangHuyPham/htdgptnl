import { useCallback, useRef, useState } from "react"
import minusIcon from "/minus.svg"
import plusIcon from "/plus.svg"
import { useLocation, useNavigate } from "react-router"

export interface MenuTabProps {
    title?: string,
    items?: TabItemProps[],
}

export interface TabItemProps {
    index: number,
    title?: string,
    navigateTo?: string
}

export interface MenuProps {
    tabs?: MenuTabProps[],
    currentIndex?: number
}

export const Menu: React.FC<{ data: MenuProps }> = ({ data }) => {
    const _tabs = useRef(data?.tabs || [])
    const [_currentIndex, setCurrentIndex] = useState<number>(data?.currentIndex ?? -1)

    const handleSelectItem = useCallback((index: number) => {
        setCurrentIndex(index)
    }, [])

    return <>
        <div className="shadow-2xl">
            {
                _tabs.current.map((v, i) => <MenuTab onSelectItem={handleSelectItem} key={i} items={v.items} title={v.title} currentIndex={_currentIndex} />)
            }
        </div>
    </>
}

const MenuTab: React.FC<MenuTabProps & { onSelectItem?: (index: number) => void, currentIndex?: number }> = ({ items, title, currentIndex = -1, onSelectItem = () => { } }) => {
    const [isHide, setHide] = useState(false)
    const location = useLocation();
    const navigate = useNavigate();

    const handleUnhide = () => {
        setHide(prev => !prev)
    }

    return <span className="text-simple-white">
        <span onClick={handleUnhide} className="flex font-semibold justify-between items-center border-b-1 border-simple-black bg-blue-500 p-2 cursor-pointer">
            <label>{title}</label>
            <span>{isHide && "➕" || "➖"}</span>
        </span>
        <span hidden={isHide}>
            <ul className="flex flex-col animate-fadeIn">
                {
                    items && items.map((v, i) => <li key={i} onClick={() => {onSelectItem(v.index); if (v?.navigateTo) navigate(v?.navigateTo)}} className={`${(location.pathname == v.navigateTo) && "bg-blue-300 text-black" || "text-simple-black bg-white"} flex min-h-[40px] font-semibold justify-between py-2 px-5 border-b-1 border-simple-black cursor-pointer hover:text-black`}>{v.title}</li>)
                }
            </ul>
        </span>
    </span>
}