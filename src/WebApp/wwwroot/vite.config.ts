import { sveltekit } from "@sveltejs/kit/vite";
import { defineConfig, loadEnv } from "vite";

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), "");

  if (mode == "development") {
    console.log(env["BACKEND_PROXY_URL"])
  }

  return {
    plugins: [sveltekit()],
    css: {
      preprocessorOptions: {
        scss: {
          additionalData: `
					@import 'src/variables.scss';
				`,
        },
      },
    },
    server: {
      proxy: {
        "/api": env["BACKEND_PROXY_URL"],
      },
    },
  };
});
