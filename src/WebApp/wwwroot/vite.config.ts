import { sveltekit } from "@sveltejs/kit/vite";
import { defineConfig, loadEnv } from "vite";

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), "");

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
        "/api": "",
      },
    },
  };
});
