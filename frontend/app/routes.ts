import {
  type RouteConfig,
  route,
} from "@react-router/dev/routes";

export default [
  route("home", "./routes/home.tsx")
] satisfies RouteConfig;