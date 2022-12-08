import Header from "../header";
import Sidebar from "../sidebar";

function DefaultLayout({ children }) {
    return (
        <div>
            <Header />
            <div className="container">
                <Sidebar />
                <div className="content">{children}</div>
            </div>

        </div>
    )
}

export default DefaultLayout;