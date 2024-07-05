<script lang="ts">
  import { waitLocale, register, init, _ } from "svelte-i18n";
  import Link from "@components/Link.svelte";
  import BreadCrumbs from "@components/BreadCrumbs.svelte";
  
  register("ru", () => import("@lib/i18n/ru.json"));

  init({
    fallbackLocale: "ru",
    initialLocale: "ru",
  });
</script>

<svelte:head>
  <title>Онлайн резюме</title>
</svelte:head>

{#await waitLocale() then}
  <body>
    <div>
      <hr />
      <Link label={$_("main-link-label")} link="/" />
      <Link label={$_("code-link-label")} link="/code" />
    </div>
    <div>
      <header>
        <BreadCrumbs />
      </header>
      <main>
        <slot />
      </main>
      <footer>
        <span aria-disabled="true">Матвеев Андрей</span>
        <a href="https://vk.com/sovetckiysouz" target="_blank">
          <img src="/img/vk.png" alt="vk-icon"/>
        </a>
        <a href="https://t.me/sovetckiysouz" target="_blank">
          <img src="/img/telegram.png" alt="telegram-icon"/>
        </a>
        <a
          href="https://github.com/andrewmat2000/tovarisch-andruha-summary/tree/rc-0.1.0"
          target="_blank"
        >
          <img src="/img/github.png" alt="github-icon"/>
        </a>
      </footer>
    </div>
  </body>
{/await}

<style lang="scss">
  main {
    position: relative;

    max-height: $theme-main-height;

    overflow: auto;
  }

  hr {
    opacity: 0.5;
  }

  body {
    position: absolute;

    top: 0;
    bottom: 0;
    left: 0;
    right: 0;

    margin: 0;

    display: grid;
    grid-template-columns: $theme-left-panel 1fr;

    > div {
      background-color: $theme-black-color;
    }

    > div:first-child {
      padding: 10px 5px;
      border-right: 1px solid $theme-white-color;
    }

    > div:last-child {
      display: grid;

      grid-template-rows: $theme-header 1fr $theme-footer;

      > header {
        border-bottom: 1px solid $theme-white-color;
        padding: 0 15px;
      }

      > footer {
        color: $theme-text-color;

        display: flex;

        align-items: center;
        justify-content: center;

        border-top: 1px solid $theme-white-color;

        span {
          -webkit-touch-callout: none;
          -webkit-user-select: none;
          -khtml-user-select: none;
          -moz-user-select: none;
          -ms-user-select: none;
          user-select: none;
        }

        a {
          background-color: transparent;
          border: none;

          img {
            height: 35px;
            width: 35px;
          }
        }

        a:hover {
          cursor: pointer;
          opacity: 0.5;
        }

        > * {
          margin-bottom: auto;
          margin-top: auto;
          padding: 0 5px;
        }
      }
    }
  }
</style>
