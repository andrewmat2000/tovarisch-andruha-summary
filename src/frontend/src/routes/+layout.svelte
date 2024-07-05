<script lang="ts">
  import { waitLocale, register, init, _ } from "svelte-i18n";
  import Link from "@components/Link.svelte";
  import BreadCrumbs from "@components/BreadCrumbs.svelte";
  import { isMobile } from "@lib";

  register("ru", () => import("@lib/i18n/ru.json"));

  init({
    fallbackLocale: "ru",
    initialLocale: "ru",
  });

  let showMenu = false;
</script>

<svelte:head>
  <title>Онлайн резюме</title>
</svelte:head>

{#await waitLocale() then}
  {#if isMobile()}
    <body class="mobile">
      {#if showMenu}
        <div id="menu">
          <Link
            label={$_("main-link-label")}
            link="/"
            callback={() => (showMenu = false)}
          />
          <Link
            label={$_("code-link-label")}
            link="/code"
            callback={() => (showMenu = false)}
          />
        </div>
      {:else}
        <div>
          <button id="menu-button" on:click={() => (showMenu = true)}>
            <div>
              <span></span>
              <span></span>
              <span></span>
            </div>
          </button>
        </div>
        <main>
          <slot />
        </main>
      {/if}
    </body>
  {:else}
    <body class="desktop">
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
            <img src="/img/vk.png" alt="vk-icon" />
          </a>
          <a href="https://t.me/sovetckiysouz" target="_blank">
            <img src="/img/telegram.png" alt="telegram-icon" />
          </a>
          <a
            href="https://github.com/andrewmat2000/tovarisch-andruha-summary/tree/rc-0.1.0"
            target="_blank"
          >
            <img src="/img/github.png" alt="github-icon" />
          </a>
        </footer>
      </div>
    </body>
  {/if}
{/await}

<style lang="scss">
  :root {
    background-color: $theme-black-color;
  }

  .desktop {
    position: absolute;

    top: 0;
    bottom: 0;
    left: 0;
    right: 0;

    margin: 0;

    display: grid;
    grid-template-columns: $theme-left-panel 1fr;

    main {
      position: relative;

      max-height: $theme-main-height;

      overflow: auto;
    }

    hr {
      opacity: 0.5;
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
  .mobile {
    #menu-button {
      background-color: transparent;
      border: none;

      width: 25px;

      padding: 0;

      div {
        display: flex;
        flex-direction: column;

        span {
          margin: 3px;
          height: 3px;

          background-color: $theme-text-color;
        }

        span:not(:last-child) {
          width: 100%;
        }

        span:last-child {
          width: 50%;
        }
      }
    }
    #menu {
      > :global(*) {
        border-bottom: 1px solid $theme-white-color;
      }
    }
  }
</style>
