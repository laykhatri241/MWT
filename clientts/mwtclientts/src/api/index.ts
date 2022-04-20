import axios from "axios";
import store from "@/store";

const config = {
  baseURL: "https://localhost:44301/",
  getAuth() {
    if (localStorage.getItem("TokenBarrier")?.length == null) {
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
          Authorization: "Bearer " + localStorage.getItem("TokenBarrier"),
        },
      };
      return config;
    }
  },
};
const call = axios.create(config);

export default call;
