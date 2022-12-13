import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { publicRoutes } from "~/routes";
import { DefaultLayout } from "~/components/layout";
import { Fragment } from "react";
import { Global } from "@emotion/react";

function App() {
  return (
    <Router>
      <div className="App">
        <Global
          styles={{
            ".some-class": {
              fontSize: 50,
              textAlign: "center",
            },
            a: {
              textDecoration: "none",
            },
          }}
        />
        <Routes>
          {publicRoutes.map((route, index) => {
            const Layout = route.layout === null ? Fragment : DefaultLayout;
            const Page = route.component;
            return (
              <Route
                key={index}
                path={route.path}
                element={
                  <Layout>
                    <Page />
                  </Layout>
                }
              />
            );
          })}
        </Routes>
      </div>
    </Router>
  );
}

export default App;
