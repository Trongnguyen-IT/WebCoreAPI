import Header from "../header";
import Sidebar from "../sidebar";
import Banner from "../banner";
import Container from "@mui/material/Container";

function DefaultLayout({ children }) {
  return (
    <Container maxWidth="xl" disableGutters>
      <Header />
      <Banner />
      <Container maxWidth="lg">
        <Sidebar />
        <div className="content">{children}</div>
      </Container>
    </Container>
  );
}

export default DefaultLayout;
