import { Header, Sidebar, Banner } from "~/layout";
import Container from "@mui/material/Container";
import Grid from "@mui/material/Grid";

function DefaultLayout({ children }) {
  return (
    <Container maxWidth="xl" disableGutters>
      <Header />
      <Banner />
      <Container maxWidth="lg">
        <Grid container spacing={2}>
          <Grid item xs={3}>
            <Sidebar />
          </Grid>
          <Grid item xs={9}>
            {/* {children} */}
          </Grid>
        </Grid>
      </Container>
    </Container>
  );
}

export default DefaultLayout;
