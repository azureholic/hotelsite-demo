import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './RunTheAppSlide.module.css'

export default function RunTheAppSlide({ index, project }) {
  return (
    <Slide index={index} className={styles.runTheAppSlide}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />

      <div className={`${styles.body} content-frame content-gutter`}>
        <div className={styles.header}>
          <p className={styles.eyebrow}>06 — Getting Started</p>
          <h1>Configure &amp; Run</h1>
          <p className={styles.subtitle}>
            Three steps from clone to running app.
          </p>
        </div>

        <div className={styles.cards}>
          <div className={styles.card}>
            <span className={styles.stepLabel}>Step 1</span>
            <h3 className={styles.cardTitle}>Azure OpenAI Config</h3>
            <p className={styles.cardText}>
              Add the endpoint and API key you receive from the organizers to{' '}
              <code>aspire\HotelSite.AppHost\appsettings.json</code> in the
              AzureOpenAI section.
            </p>
          </div>
          <div className={styles.card}>
            <span className={styles.stepLabel}>Step 2</span>
            <h3 className={styles.cardTitle}>Start the App</h3>
            <p className={styles.cardText}>
              Run <code>.\run.ps1</code> from the repo root. It installs
              frontend deps on first run and starts the full app via .NET
              Aspire.
            </p>
          </div>
          <div className={styles.card}>
            <span className={styles.stepLabel}>Step 3</span>
            <h3 className={styles.cardTitle}>Open the Dashboard</h3>
            <p className={styles.cardText}>
              Open the Aspire dashboard URL from the terminal. Click the
              frontend endpoint to access the hotel booking site at{' '}
              <code>localhost:5173</code>.
            </p>
          </div>
        </div>

        <div className={styles.callout}>
          <p>
            <strong>Console Agent:</strong> In a second terminal, run{' '}
            <code>cd console-agent\ConsoleAgent &amp;&amp; dotnet run</code> to
            start the interactive chat agent connected to the backend MCP
            server.
          </p>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot" />
    </Slide>
  )
}
