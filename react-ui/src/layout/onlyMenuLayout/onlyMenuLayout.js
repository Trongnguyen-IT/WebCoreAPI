import { Header } from "~/layout";
import Container from "@mui/material/Container";

function OnlyMenuLayout({ children }) {
  return (
    <Container maxWidth="xl" disableGutters>
      <Header />
      <Container maxWidth="lg">{children}</Container>
    </Container>
  );
}

export default OnlyMenuLayout;
