import axios from "axios";
import store from "@/store";
// localStorage.getItem("UserId")
const config = {
  baseURL: "https://localhost:44301/",
  getAuth() {
    debugger
    if (localStorage.getItem("Token")?.length == null) {
      const config = {
        headers: {
          Accept: "application/json",
        },
      };
      return config;
    } else {
      const config = {
        headers: {
          Accept: "application/json",
          Authorization: "Bearer " + localStorage.getItem("Token"),
        },
      };
      return config;
    }
  },
};
const call = axios.create(config);

export default call;

