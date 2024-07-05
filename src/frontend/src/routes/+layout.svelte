<script lang="ts">
  import { waitLocale, register, init, _ } from "svelte-i18n";
  import Link from "@components/Link.svelte";
  import BreadCrumbs from "@components/BreadCrumbs.svelte";
  import { isMobile } from "@lib";
  import { uiStore } from "@lib/stores/ui-store";

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
        <button id="menu" on:click={() => (showMenu = false)}>
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
        </button>
      {/if}
      <header>
        <button
          id="back-button"
          class={`arrow`}
          style={`opacity: ${$uiStore.url.length > 1 ? "1" : "0"};`}
          on:click={() => uiStore.goBack()}
        >
          <div></div>
        </button>
        <span></span>
        <button id="menu-button" on:click={() => (showMenu = true)}>
          <div>
            <span></span>
            <span></span>
            <span></span>
          </div>
        </button>
      </header>
      <main>
        <slot />
      </main>
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
    grid-template-columns: $desktop-left-panel-width 1fr;

    hr {
      opacity: 0.5;
    }

    > div:first-child {
      padding: 10px 5px;
      border-right: 1px solid $theme-white-color;
    }

    header {
      position: fixed;
      top: 0;
      bottom: calc(100vh - $desktop-header-height);
      left: $desktop-left-panel-width;
      right: 0;
      border-bottom: 1px solid $theme-white-color;
      padding: 0 $desktop-default-padding;
    }

    main {
      position: fixed;

      top: $desktop-header-height;
      bottom: $desktop-footer-height;
      left: calc($desktop-left-panel-width);
      right: 0px;

      padding: 0 15px;

      overflow: auto;

      padding-bottom: $desktop-default-padding;
    }

    footer {
      position: fixed;

      right: 0;
      left: $desktop-left-panel-width;
      top: calc(100vh - $desktop-footer-height);
      bottom: 0;

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
  .mobile {
    #menu-button {
      background-color: transparent;

      display: flex;

      border: none;

      margin: 5px;
      padding: 0;

      div {
        display: flex;
        flex-direction: column;

        height: 100%;
        width: 100%;

        span {
          margin: 3px;
          height: 5px;

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

    header {
      $width: calc($mobile-header-height - 10px);
      $height: calc($mobile-header-height - 10px);

      position: fixed;

      top: $mobile-default-padding;
      bottom: calc(100vh - $mobile-header-height);
      left: $mobile-default-padding;
      right: $mobile-default-padding;

      display: grid;
      grid-template-columns: $width 1fr $width + 10px;

      > * {
        width: $width;
        height: $height;
      }
    }

    main {
      position: fixed;

      top: $mobile-header-height;
      left: 10px;
      right: 10px;
      bottom: 0;

      padding-bottom: $mobile-default-padding;

      overflow: auto;
    }

    footer {
    }
    @keyframes menuOpen {
      from {
        margin-right: 100vw;
      }

      to {
        margin-right: 0;
      }
    }

    #menu {
      z-index: 1;

      display: flex;
      align-items: flex-start;
      flex-direction: column;

      border: none;
      position: absolute;

      top: 0;
      bottom: 0;
      right: 0;
      left: 0;

      padding-top: $mobile-header-height;

      animation: menuOpen 0.6s;
      background: $theme-black-color;
    }

    .arrow {
      $multiple: 1.1;

      border: none;
      background-color: transparent;

      height: calc($mobile-header-height * $multiple);
      width: calc($mobile-header-height * $multiple);

      div {
        display: flex;

        height: 100%;
        width: 100%;

        // background-color: transparent;
        background: url("/img/arrow.png") no-repeat;
        background-position: center;
        background-size: contain;
      }
    }
  }
</style>
