import Header from "../header";
import Sidebar from "../sidebar";
import Banner from "../banner";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

function DefaultLayout({ children }) {
  return (
    <Container maxWidth="xl" disableGutters>
      <Header />
      <Banner />
      <Container maxWidth="lg">
        <Grid container spacing={2}>
          <Grid item xs={4}>
            <Sidebar />
          </Grid>
          <Grid item xs={8}>
            <div className="content">{children}</div>
          </Grid>
        </Grid>
      </Container>
    </Container>
  );
}

export default DefaultLayout;
