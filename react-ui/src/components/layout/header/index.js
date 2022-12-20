import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import AdbIcon from "@mui/icons-material/Adb";
import { publicRoutes as routes } from "~/routes";
import { Link } from "react-router-dom";

function Header() {
  return (
    <AppBar position="static" color="transparent">
      <Container maxWidth="lg">
        <Toolbar disableGutters>
          <AdbIcon sx={{ display: { xs: "none", md: "flex" }, mr: 1 }} />
          <Typography
            variant="h6"
            noWrap
            component="a"
            href="/"
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            LOGO
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
            {routes.map((item, index) => (
              <Link
                key={index}
                css={{
                  marginRight: "1rem",
                  color: "#333",
                  display: "inline-block",
                  textDecoration: "none",
                  "&:hover": {
                    color: "#f16821",
                    transitionDuration: "0.4s",
                    WebkitTransitionDuration: "0.4s",
                    msTransitionDuration: "0.4s",
                  },
                }}
                to={item.path}
              >
                {item.name}
              </Link>
            ))}
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
}
export default Header;
