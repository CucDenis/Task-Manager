import { Provider } from "react-redux";
import { store } from "./redux/store";
import { Routes, Route } from "react-router-dom";
import { ProtectedRoute } from "../features/auth/components/ProtectedRoute";
import { LoginForm } from "../features/log-in/components/LoginForm";
import { InterventionFileContainer } from "../features/intervention-file/containers/InterventionFileContainer";
import { InterventionFileCreateForm } from "../features/intervention-file/components/InterventionFileCreateForm";
import { Header } from "../features/layout/components/Header";
import { Footer } from "../features/layout/components/Footer";

export const App = () => {
  return (
    <Provider store={store}>
      <div className="app-container min-vh-100 d-flex flex-column">
        <Header />
        <main className="container py-4 flex-grow-1">
          <Routes>
            <Route path="/login" element={<LoginForm />} />

            <Route element={<ProtectedRoute />}>
              <Route path="/" element={<InterventionFileContainer />} />
              <Route
                path="/interventions/new"
                element={<InterventionFileCreateForm />}
              />
            </Route>
          </Routes>
        </main>
        <Footer />
      </div>
    </Provider>
  );
};
