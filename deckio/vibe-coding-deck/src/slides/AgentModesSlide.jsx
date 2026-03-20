import { BottomBar, Slide } from '@deckio/deck-engine'
import styles from './AgentModesSlide.module.css'

export default function AgentModesSlide({ index, project }) {
  return (
    <Slide index={index} className={styles.agentModesSlide}>
      <div className="accent-bar" />
      <div className={`orb ${styles.orb1}`} />
      <div className={`orb ${styles.orb2}`} />

      <div className={`${styles.body} content-frame content-gutter`}>
        <div className={styles.header}>
          <p className={styles.eyebrow}>03 — Deep Dive</p>
          <h1>Agent Modes &amp; Capabilities</h1>
          <p className={styles.subtitle}>
            Agents run where the work happens — choose the right mode for each
            task.
          </p>
        </div>

        <div className={styles.cards}>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>Plan → Build</h3>
            <p className={styles.cardText}>
              The Plan agent breaks a feature into steps, analyzes your
              codebase, and hands off to an implementation agent when you
              approve.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>
              Local · Background · Cloud
            </h3>
            <p className={styles.cardText}>
              Run interactively in VS Code, autonomously in the background, or
              as a cloud agent that opens a PR for team review.
            </p>
          </div>
          <div className={styles.card}>
            <h3 className={styles.cardTitle}>
              Multi-Session &amp; Delegation
            </h3>
            <p className={styles.cardText}>
              Run parallel sessions on different tasks. Hand off between agent
              types — context carries over seamlessly.
            </p>
          </div>
        </div>
      </div>

      <BottomBar text="Vibe Coding with GitHub Copilot CLI" />
    </Slide>
  )
}
