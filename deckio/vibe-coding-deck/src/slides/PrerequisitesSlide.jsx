import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './PrerequisitesSlide.module.css'

export default function PrerequisitesSlide({ index, project }) {
  return (
    <Slide index={index} className={styles.prerequisitesSlide}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />

      <div className={`${styles.body} content-frame content-gutter`}>
        <div className={styles.header}>
          <p className={styles.eyebrow}>05 — Setup</p>
          <h1>Prerequisites &amp; Authentication</h1>
          <p className={styles.subtitle}>
            Run one script, sign in twice, and you&apos;re ready to hack.
          </p>
        </div>

        <div className={styles.cards}>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Install Everything</h3>
            <p className={styles.cardText}>
              Run <code>.\prep-env.ps1</code> to install .NET 10, Node.js,
              Git, GitHub CLI + Copilot extension, Azure CLI, and VS Code via
              winget.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Sign In — VS Code</h3>
            <p className={styles.cardText}>
              Click Accounts → Sign in with GitHub to use GitHub Copilot.
              Authorize in the browser. Copilot icon appears in the status bar.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Sign In — CLI</h3>
            <p className={styles.cardText}>
              Run <code>gh auth login</code>, then just type{' '}
              <code>copilot</code>. It picks up your auth automatically. Add{' '}
              <code>--banner</code> for the splash screen.
            </p>
          </div>
        </div>

        <div className={styles.callout}>
          <p>
            <strong>Tip:</strong> Restart your terminal after installation for
            PATH changes to take effect.
          </p>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot CLI" />
    </Slide>
  )
}
