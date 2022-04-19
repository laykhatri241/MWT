import axios from "axios";
import store from "@/store";
const accessToken = localStorage.getItem("Token");

const config = {
  baseURL: "https://localhost:44301/Account",
  headers: { Accept: "application/json"},
};
const call = axios.create(config);

export default call;
